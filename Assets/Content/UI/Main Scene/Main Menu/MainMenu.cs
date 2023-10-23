using FightParty.Audio;
using System;

namespace FightParty.UI.MainScene
{
    public class MainMenu : IDisposable
    {
        public event Action ClickedPlay;
        public event Action ClickedCollection;
        public event Action ClickedSettings;

        private MainMenuView _view;

        private GlobalSFXSource _audio;

        public MainMenu(MainMenuView view, GlobalSFXSource audio)
        { 
            _view = view;

            _view.PlayButton.onClick.AddListener(ClickPlayButton);
            _view.CollectionButton.onClick.AddListener(ClickCollectionButton);
            _view.SettingsButton.onClick.AddListener(ClickSettingsButton);

            _audio = audio;
        }

        public MainMenuView View => _view;

        public void Dispose()
        {
            _view.PlayButton.onClick.RemoveAllListeners();
            _view.CollectionButton.onClick.RemoveAllListeners();
            _view.SettingsButton.onClick.RemoveAllListeners();
        }

        private void ClickPlayButton()
        {
            _audio.PlayClick();

            ClickedPlay?.Invoke();
        }

        private void ClickCollectionButton()
        {
            _audio.PlayClick();

            ClickedCollection?.Invoke();
        }

        private void ClickSettingsButton()
        {
            _audio.PlayClick();

            ClickedSettings?.Invoke();
        }
    }
}