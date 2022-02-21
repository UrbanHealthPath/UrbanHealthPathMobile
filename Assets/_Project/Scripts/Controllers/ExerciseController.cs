using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Controllers
{
    /// <summary>
    /// Controller responsible for showing appropriate popup for given exercise.
    /// </summary>
    public class ExerciseController : BaseController
    {
        private readonly CoroutineManager _coroutineManager;

        public ExerciseController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager)
            : base(viewManager, popupManager)
        {
            _coroutineManager = coroutineManager;
        }

        public void ShowPopupForExercise(Exercise exercise)
        {
            _coroutineManager.StartCoroutine(CreatePopupForExercise(exercise));
        }

        private IEnumerator CreatePopupForExercise(Exercise exercise)
        {
            yield return new WaitForEndOfFrame();
            IPopupable popupableView = ViewManager.CurrentView.GetComponent<IPopupable>();

            ClearPopup();

            switch (exercise.Levels[0])
            {
                case VideoExerciseLevel videoExerciseLevel:
                    PopupManager.OpenPopup(PopupType.WithTextAndVideo,
                        new PopupWithTextAndVideoInitializationParameters(exercise.DisplayedName,
                            videoExerciseLevel.Description,
                            new VideoFileAccessor(videoExerciseLevel.VideoFile).GetMedia(),
                            new PopupPayload(popupableView.PopupArea)));
                    break;
                case ImageExerciseLevel imageExerciseLevel:
                    PopupManager.OpenPopup(PopupType.WithTextAndImage,
                        new PopupWithTextAndImageInitializationParameters(exercise.DisplayedName,
                            imageExerciseLevel.Description,
                            new TextureFileAccessor(imageExerciseLevel.ImageFile).GetMedia(),
                            new PopupPayload(popupableView.PopupArea)));
                    break;
                case ImageSelectionExerciseLevel imageSelectionExerciseLevel:
                    List<QuizElementOption> imageQuizElementOptions = new List<QuizElementOption>();

                    foreach (LateBoundValue<MediaFile> image in imageSelectionExerciseLevel.Images)
                    {
                        Texture2D texture = new TextureFileAccessor(image).GetMedia();
                        bool isCorrect =
                            imageSelectionExerciseLevel.CorrectAnswers.Contains(
                                imageSelectionExerciseLevel.Images.IndexOf(image));

                        imageQuizElementOptions.Add(new QuizElementOption(texture,
                            (button) =>
                            {
                                if (isCorrect)
                                {
                                    button.EnableGreenFrame();
                                }
                                else
                                {
                                    button.EnableRedFrame();
                                }
                            }));
                    }

                    PopupManager.OpenPopup(PopupType.QuizWithImages,
                        new QuizWithImagesPopupInitializationParameters(imageSelectionExerciseLevel.Question,
                            new PopupPayload(popupableView.PopupArea),
                            imageQuizElementOptions.ToArray()
                        ));
                    break;
                case AnswerSelectionExerciseLevel answerSelectionExerciseLevel:
                    List<QuizWithTextElementOption> textQuizElementOptions = new List<QuizWithTextElementOption>();

                    foreach (string answer in answerSelectionExerciseLevel.Answers)
                    {
                        bool isCorrect =
                            answerSelectionExerciseLevel.CorrectAnswers.Contains(
                                answerSelectionExerciseLevel.Answers.IndexOf(answer));

                        textQuizElementOptions.Add(new QuizWithTextElementOption(answer,
                            (button) =>
                            {
                                if (isCorrect)
                                {
                                    button.EnableGreenFrame();
                                }
                                else
                                {
                                    button.EnableRedFrame();
                                }
                            }));
                    }

                    PopupManager.OpenPopup(PopupType.QuizWithTexts,
                        new QuizWithTextPopupInitializationParameters(answerSelectionExerciseLevel.Question,
                            new PopupPayload(popupableView.PopupArea),
                            textQuizElementOptions.ToArray()
                        ));
                    break;
                case TextExerciseLevel textExerciseLevel:
                    PopupManager.OpenPopup(PopupType.WithTextAndImage,
                        new PopupWithTextAndImageInitializationParameters(exercise.DisplayedName,
                            textExerciseLevel.Description,
                            null,
                            new PopupPayload(popupableView.PopupArea)));
                    break;
                default:
                    break;
            }
        }

        private void ClearPopup()
        {
            if (PopupManager.CurrentPopupType != PopupType.None)
            {
                PopupManager.CloseCurrentPopup();
            }
        }
    }
}