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


    [Serializable]
    public class BlockSettings
    {
        public BlockTunables BlockTunables;
        public BlockMove.Settings BlockMove;
        public MoveBlockGroupTransform.Settings MoveBlockGroup;
    }

    [Serializable]
    public class PlayerSettings
    {
        public PlayerMoveImitator.Settings PlayerMoveImitator;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(BlocksSpawner).IfNotBound();
        Container.BindInstance(GameInstaller).IfNotBound();

        Container.BindInstance(Block.BlockMove).IfNotBound();

        Container.BindInstance(Block.MoveBlockGroup).IfNotBound();


        Container.BindInstance(Player.PlayerMoveImitator).IfNotBound();
    }

    private void OnValidate()
    {
        Block.MoveBlockGroup.moveDuration = Player.PlayerMoveImitator.moveDuration;

    }
}
