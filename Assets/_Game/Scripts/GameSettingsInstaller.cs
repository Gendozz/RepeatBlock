using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

[CreateAssetMenu(menuName = "RepeatBlock/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameInstaller.Settings GameInstaller;
    [Space]
    public BlocksSpawner.Settings BlocksSpawner;
    public BlockPathGenerator.Settings BlockPathGenerator;
    public BlockSettings Block;
    public AllBlocksMovedChecker.Settings AllBlocksMovedChecker;
    
    [Space]
    public PlayerSettings Player;
    [Space]
    public RestartGame.Settings RestartGame;


    [Serializable]
    public class BlockSettings
    {
        public BlockTunables BlockTunables;
        public BlockMoveUpDown.Settings BlockMoveUpDown;
        public BlockMoveOnPlayerMove.Settings BlockMoveOnPlayerMove;
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
        Container.BindInstance(GameInstaller).IfNotBound();

        Container.BindInstance(BlocksSpawner).IfNotBound();
        Container.BindInstance(BlockPathGenerator).IfNotBound();
        Container.BindInstance(Block.BlockMoveUpDown).IfNotBound();
        Container.BindInstance(Block.BlockMoveOnPlayerMove).IfNotBound();
        Container.BindInstance(Block.BlockOutOfViewChecker).IfNotBound();
        Container.BindInstance(AllBlocksMovedChecker).IfNotBound();
        

        Container.BindInstance(Player.PlayerMoveImitator).IfNotBound();

        Container.BindInstance(RestartGame).IfNotBound();
    }

    private void OnValidate()
    {
        Block.BlockMoveOnPlayerMove.moveDuration = Player.PlayerMoveImitator.moveDuration;

    }
}
