using Leopotam.Ecs;
public class UnitAnimationsSystem : IEcsRunSystem
{
    EcsFilter<UnitAnimationComponent, SetAnimation> animations = null;
    public void Run()
    {
        foreach (var item in animations)
        {
            ref var animator = ref animations.Get1(item);
            var targetState = animations.Get2(item).Type;
            if(animator.unitAnimations.TryGetAnimByType(targetState, out var material))
            {
                animator.meshRenderer.sharedMaterial = material;
            }
            animations.GetEntity(item).Del<SetAnimation>();
        }
    }
}
