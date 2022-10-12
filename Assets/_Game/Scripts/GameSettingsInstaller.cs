using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "RepeatBlock/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public BlocksSpawner.Settings BlocksSpawner;
    public GameInstaller.Settings GameInstaller;
    public BlockSettings Block;
    public PlayerSettings Player;

    public RestartGame.Settings RestartGame;


    [Serializable]
    public class BlockSettings
    {
        public BlockTunables BlockTunables;
        public BlockMoveUpDown.Settings BlockMove;
        public BlockMoveOnPlayerMove.Settings MoveBlockGroup;
        public BlockOutOfViewChecker.Settings BlockOutOfViewChecker;
    }

    [Serializable]
    public class PlayerSettings
    {
        public PlayerMoveImitator.Settings PlayerMoveImitator;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.BindInstance(BlocksSpawner).IfNotBound();
        Container.BindInstance(GameInstaller).IfNotBound();

        Container.BindInstance(Block.BlockMove).IfNotBound();
        Container.BindInstance(Block.MoveBlockGroup).IfNotBound();
        Container.BindInstance(Block.BlockOutOfViewChecker).IfNotBound();

        Container.BindInstance(Player.PlayerMoveImitator).IfNotBound();

        Container.BindInstance(RestartGame).IfNotBound();
    }

    private void OnValidate()
    {
        Block.MoveBlockGroup.moveDuration = Player.PlayerMoveImitator.moveDuration;

    }
}
