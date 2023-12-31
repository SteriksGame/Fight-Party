using FightParty.Game;
using FightParty.Game.SceneLoader;
using FightParty.Save;
using UnityEngine;
using Zenject;

namespace FightParty.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private SettingsManagerConfig _settingsManagerConfig;
        [SerializeField] private ProgressManagerConfig _progressManagerConfig;

        [Space]
        [SerializeField] private ScreenFader _screenFader;

        public override void InstallBindings()
        {
            BindCallBackTimer();

            BindSaveManagers();

            BindScreenFader();

            BindSceneLoader();
        }

        private void BindCallBackTimer() 
            => Container.Bind<CallBackTimer>().FromNew().AsSingle().WithArguments(this);

        private void BindSaveManagers()
        {
            Container.BindInstance(_settingsManagerConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsManager>().FromNew().AsSingle();

            Container.BindInstance(_progressManagerConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<ProgressManager>().FromNew().AsSingle();
        }

        private void BindScreenFader()
        {
            ScreenFader screenFader = Instantiate(_screenFader, transform);

            Container.BindInstances(screenFader);
        }

        private void BindSceneLoader()
        {
            Container.Bind<ZenjectSceneLoaderWrapper>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoadMediator>().FromNew().AsSingle(); 
        }
    }
}