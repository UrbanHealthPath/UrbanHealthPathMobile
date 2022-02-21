using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    /// <summary>
    /// Class that is able to parse JObject into general ExerciseLevel.
    /// </summary>
    public class ExerciseLevelJsonParser : ValidatedJsonObjectParser<ExerciseLevel>
    {
        private const string TYPE_KEY = "type";

        private readonly IDictionary<string, IParser<JObject, ExerciseLevel>> _registeredTypesParsers;

        public ExerciseLevelJsonParser(JsonObjectParser<TextExerciseLevel> textExerciseParser,
            JsonObjectParser<VideoExerciseLevel> videoExerciseParser,
            JsonObjectParser<ImageExerciseLevel> imageExerciseParser,
            JsonObjectParser<ImageSelectionExerciseLevel> imageSelectionExerciseParser,
            JsonObjectParser<AnswerSelectionExerciseLevel> answerSelectionExerciseParser,
            JsonObjectParser<HistoricalFactExerciseLevel> historicalFactExerciseParser) : base(new[] {TYPE_KEY})
        {
            _registeredTypesParsers = new Dictionary<string, IParser<JObject, ExerciseLevel>>();
            _registeredTypesParsers.Add("text", textExerciseParser);
            _registeredTypesParsers.Add("video", videoExerciseParser);
            _registeredTypesParsers.Add("image", imageExerciseParser);
            _registeredTypesParsers.Add("answer_selection", answerSelectionExerciseParser);
            _registeredTypesParsers.Add("image_selection", imageSelectionExerciseParser);
            _registeredTypesParsers.Add("historical_fact", historicalFactExerciseParser);
        }

        protected override void ValidateJson(JObject json)
        {
            base.ValidateJson(json);

            string parserKey = json[TYPE_KEY].Value<string>();
            
            if (!_registeredTypesParsers.ContainsKey(parserKey))
            {
                throw new ParsingException($"Parser with type {parserKey} not found!");
            }
        }

        protected override ExerciseLevel ParseJsonObject(JObject json)
        {
            return _registeredTypesParsers[json[TYPE_KEY].Value<string>()].Parse(json);
        }
    }
}