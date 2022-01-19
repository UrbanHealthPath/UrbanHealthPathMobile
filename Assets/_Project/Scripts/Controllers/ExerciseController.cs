using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManager;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class ExerciseController : BaseController
    {
        public Action<Exercise> ExerciseFinished;
        
        private readonly PopupManager _popupManager;
        private readonly CoroutineManager _coroutineManager;

        public ExerciseController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager) : base(viewManager)
        {
            _popupManager = popupManager;
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
                    _popupManager.OpenPopup(PopupType.WithTextAndVideo,
                        new PopupWithTextAndVideoInitializationParameters(exercise.DisplayedName,
                            videoExerciseLevel.Description,
                            new VideoFileAccessor(videoExerciseLevel.VideoFile).GetMedia(),
                            new PopupPayload(popupableView.PopupArea)));
                    break;
                case ImageExerciseLevel imageExerciseLevel:
                    _popupManager.OpenPopup(PopupType.WithTextAndImage,
                        new PopupWithTextAndImageInitializationParameters(exercise.DisplayedName,
                            imageExerciseLevel.Description,
                            new TextureFileAccessor(imageExerciseLevel.ImageFile).GetMedia(),
                            new PopupPayload(popupableView.PopupArea)));
                    break;
                case ImageSelectionExerciseLevel imageSelectionExerciseLevel:
                    List<QuizElementOption> quizElementOptions = new List<QuizElementOption>();

                    foreach (LateBoundValue<MediaFile> image in imageSelectionExerciseLevel.Images)
                    {
                        Texture2D texture = new TextureFileAccessor(image).GetMedia();
                        bool isCorrect = imageSelectionExerciseLevel.CorrectAnswers.Contains(imageSelectionExerciseLevel.Images.IndexOf(image));

                        quizElementOptions.Add(new QuizElementOption(texture,
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

                    _popupManager.OpenPopup(PopupType.QuizWithImages,
                        new QuizWithImagesPopupInitializationParameters(imageSelectionExerciseLevel.Question,
                            new PopupPayload(popupableView.PopupArea),
                            quizElementOptions.ToArray()
                        ));
                    break;
                case TextExerciseLevel textExerciseLevel:
                    _popupManager.OpenPopup(PopupType.WithTextAndImage,
                        new PopupWithTextAndImageInitializationParameters(exercise.DisplayedName,
                            textExerciseLevel.Description,
                            null,
                            new PopupPayload(popupableView.PopupArea)));
                    break;
                default:
                    break;
            }
        }

        private void OnExerciseFinished(Exercise exercise)
        {
            ExerciseFinished?.Invoke(exercise);
        }
        
        private void ClearPopup()
        {
            if (_popupManager.CurrentPopupType != PopupType.None)
            {
                _popupManager.CloseCurrentPopup();
            }
        }
    }
}