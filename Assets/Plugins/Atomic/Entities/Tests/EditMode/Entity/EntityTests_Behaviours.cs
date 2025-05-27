using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        [Test]
        public void GetAllBehaviours()
        {
            //Arrange:
            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity(behaviours: new IEntityBehaviour[]
            {
                updateStub,
                initStub,
                behaviourStub
            });

            //Act:
            IReadOnlyCollection<IEntityBehaviour> behaviours = entity.GetBehaviours();

            //Assert:
            Assert.AreEqual(new HashSet<IEntityBehaviour>
            {
                updateStub,
                initStub,
                behaviourStub
            }, behaviours);
        }

        [Test]
        public void HasBehaviour()
        {
            //Arrange:
            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity(behaviours: new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            //Assert & Act:
            Assert.IsTrue(entity.HasBehaviour(updateStub));
            Assert.IsTrue(entity.HasBehaviour<EntityInitStub>());
            Assert.IsFalse(entity.HasBehaviour(behaviourStub));
        }

        //TODO:
        [Test]
        public void AddBehaviour()
        {
            //Arrange:
            IEntityBehaviour addedBehaviour = null;

            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();

            var entity = new Entity(behaviours: new IEntityBehaviour[]
            {
                updateStub
            });

            entity.OnBehaviourAdded += (_, b) => addedBehaviour = b;

            //Assert & Act:
            // Assert.IsFalse(entity.AddBehaviour(updateStub));
            // Assert.IsNull(addedBehaviour);

            // Assert.IsTrue(entity.AddBehaviour(initStub));
            // Assert.AreEqual(initStub, addedBehaviour);

            // entity.AddBehaviour<EntityBehaviourStub>();
            // Assert.IsNotNull(addedBehaviour);
        }

        [Test]
        public void DelBehaviour()
        {
            //Arrange:
            IEntityBehaviour removedBehaviour = null;

            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity(behaviours: new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            entity.OnBehaviourDeleted += (_, b) => removedBehaviour = b;

            //Assert & Act:
            Assert.IsTrue(entity.DelBehaviour(updateStub));
            Assert.AreEqual(updateStub, removedBehaviour);

            Assert.IsTrue(entity.DelBehaviour<EntityInitStub>());
            Assert.IsFalse(entity.HasBehaviour(initStub));

            Assert.IsFalse(entity.DelBehaviour(behaviourStub));
            Assert.IsFalse(entity.DelBehaviour<EntityInitStub>());
        }

        [Test]
        public void WhenAddBehaviourAfterInitThenBehaviourWiilInit()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity();
            entity.Init();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsFalse(behaviourStub.enabled);
            Assert.AreEqual(nameof(EntityBehaviourStub.Init), behaviourStub.invokationList[0]);
        }

        [Test]
        public void WhenAddBehaviourAfterEnableThenBehaviourWillEnable()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity();
            entity.Init();
            entity.Enable();

            //Act
            entity.AddBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.initialized);
            Assert.IsTrue(behaviourStub.enabled);
            Assert.AreEqual(nameof(EntityBehaviourStub.Init), behaviourStub.invokationList[0]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Enable), behaviourStub.invokationList[1]);
        }

        [Test]
        public void WhenDelBehaviourAfterInitThenBehaviourWiilDispose()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity(behaviours: new IEntityBehaviour[] {behaviourStub});
            entity.Init();

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsFalse(behaviourStub.disabled);
            Assert.IsTrue(behaviourStub.disposed);
        }

        [Test]
        public void WhenDelBehaviourAfterEnableThenBehaviourWiilDisableAndDispose()
        {
            //Arrange:
            var behaviourStub = new EntityBehaviourStub();

            var entity = new Entity(behaviours: new IEntityBehaviour[] {behaviourStub});
            entity.Init();
            entity.Enable();

            //Act
            entity.DelBehaviour(behaviourStub);

            Assert.IsTrue(behaviourStub.disabled);
            Assert.IsTrue(behaviourStub.disposed);
            Assert.AreEqual(nameof(EntityBehaviourStub.Disable), behaviourStub.invokationList[^2]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Dispose), behaviourStub.invokationList[^1]);
        }

        [Test]
        public void ClearBehaviours()
        {
            var updateStub = new EntityUpdateStub();
            var initStub = new EntityInitStub();

            var entity = new Entity(behaviours: new IEntityBehaviour[]
            {
                updateStub,
                initStub
            });

            // entity.OnBehavioursCleared += _ => wasClear = true;

            //Act:
            entity.ClearBehaviours();

            //Assert:
            Assert.AreEqual(0, entity.BehaviourCount);
        }

        //TODO:
        // [Test]
        // public void WhenClearEmptyBehavioursThenFalse()
        // {
        //     //Arrange:
        //     // var wasClear = false;
        //     var entity = new Entity();
        //
        //     // entity.OnBehavioursCleared += _ => wasClear = true;
        //
        //     //Act:
        //     bool success = entity.ClearBehaviours();
        //
        //     Assert.IsFalse(success);
        //     // Assert.IsFalse(wasClear);
        // }
    }
}