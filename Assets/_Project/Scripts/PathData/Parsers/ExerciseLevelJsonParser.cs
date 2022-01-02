using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    public class ExerciseLevelJsonParser : ValidatedJsonObjectParser<ExerciseLevel>
    {
        private const string TYPE_KEY = "type";

        private readonly IDictionary<string, IParser<JObject, ExerciseLevel>> _registeredTypesParsers;

        public ExerciseLevelJsonParser(JsonObjectParser<TextExerciseLevel> textExerciseParser) : base(new []{TYPE_KEY})
        {
            _registeredTypesParsers = new Dictionary<string, IParser<JObject, ExerciseLevel>>();
            _registeredTypesParsers.Add("text", textExerciseParser);
        }

        protected override void ValidateJson(JObject json)
        {
            base.ValidateJson(json);

            if (!_registeredTypesParsers.ContainsKey(json[TYPE_KEY].Value<string>()))
            {
                throw new ParsingException();
            }
        }

        protected override ExerciseLevel ParseJsonObject(JObject json)
        {
            return _registeredTypesParsers[json[TYPE_KEY].Value<string>()].Parse(json);
        }
        
    }
}