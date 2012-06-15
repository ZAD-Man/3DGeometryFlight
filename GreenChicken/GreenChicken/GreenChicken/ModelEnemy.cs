﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GreenChicken
{
    internal class ModelEnemy : Enemy
    {
        public static PlayerModel player;
        private static BasicEffect effect;
        protected float _boundingSphereSize = 4.0f;
        protected float speed;
        protected Vector3 target;

        public ModelEnemy(float spd, Model model, bool collidable = true)
        {
            IsCollidable = collidable;
            if(IsCollidable)
                CollisionManager.GetInstance(null).AddToCollidables(this);
            this.model = model;
            speed = spd;
            if(effect == null)
                effect = new BasicEffect(Game1.GameInstance.GraphicsDevice);
        }

        public Model model { get; protected set; }
        
        #region Implemented from Basic

        public override void Draw(Camera camera)
        {
            Game1.GameInstance.GraphicsDevice.RasterizerState = new RasterizerState { FillMode = FillMode.WireFrame };

            effect.World = World;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.LightingEnabled = true;
            effect.EmissiveColor = new Vector3(.7f, 0, 1);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart mmp in mesh.MeshParts)
                {
                    mmp.Effect = effect;
                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                    }
                }
                mesh.Draw();
            }
        }

        protected override BoundingSphere GetBoundingSphere()
        {
            return new BoundingSphere(Position, _boundingSphereSize);
        }

        #endregion
    }
}