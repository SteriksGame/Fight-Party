using FightParty.Audio;
using I2.Loc;
using System;
using UnityEngine;

namespace FightParty.Game.MainScene
{
    public partial class CollectionMenu : IDisposable
    {
        private const string Localization_BattleSkinInfo_Key = "Interfaces/CollectionMenu-2";
        private const string Localization_SurvivalSkinInfo_Key = "Interfaces/CollectionMenu-3";

        public event Action SelectedRing;
        public event Action<int> ChangedRing;

        private int _currentRing = 0;
        private int _maxRingIndex = 2;

        private CollectionMenuView _view;

        private GlobalSFXSource _audio;

        private SkinChanger _skinChanger;

        public CollectionMenu(CollectionMenuView view, GlobalSFXSource audio, SkinChanger skinChanger)
        {
            _view = view;

            _view.LeftButton.onClick.AddListener(SwitchRingToLeft);
            _view.SelectButton.onClick.AddListener(SelectRing);
            _view.RightButton.onClick.AddListener(SwitchRingToRight);

            _audio = audio;

            _skinChanger = skinChanger;

            _currentRing = _skinChanger.IndexCurrentSkin;
        }

        public IOpenClose View => _view;

        public void Dispose()
        {
            _view.LeftButton.onClick.RemoveListener(SwitchRingToLeft);
            _view.SelectButton.onClick.RemoveListener(SelectRing);
            _view.RightButton.onClick.RemoveListener(SwitchRingToRight);
        }

        private void SwitchRingToLeft()
        {
            _audio.PlayClick();

            if (_currentRing == 0)
                _currentRing = _maxRingIndex;
            else
                _currentRing--;

            UpdateCurrentSkin();

            ChangedRing?.Invoke(_currentRing);
        }

        private void SwitchRingToRight()
        {
            _audio.PlayClick();

            if (_currentRing == _maxRingIndex)
                _currentRing = 0;
            else
                _currentRing++;

            UpdateCurrentSkin();

            ChangedRing?.Invoke(_currentRing);
        }

        private void SelectRing()
        {
            if(_skinChanger.IsCloseTexture())
                _audio.PlayLock();
            else
            {
                _audio.PlayClick();

                SelectedRing?.Invoke();
            }
        }

        private void UpdateCurrentSkin()
        {
            _view.CloseLockPanel();

            switch(_currentRing)
            {
                case 0:
                    _skinChanger.SetSkin(RingSkinTypes.Default);
                    break;

                case 1:
                    if(_skinChanger.SetSkin(RingSkinTypes.Battle) == false)
                        _view.OpenLockPanel(LocalizationManager.GetTermTranslation(Localization_BattleSkinInfo_Key));
                    break;

                case 2:
                    if (_skinChanger.SetSkin(RingSkinTypes.Survival) == false)
                        _view.OpenLockPanel(LocalizationManager.GetTermTranslation(Localization_SurvivalSkinInfo_Key));
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
