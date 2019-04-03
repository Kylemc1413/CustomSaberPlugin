using System;
using UnityEngine;
using Xft;


namespace CustomSaber
{
    class CustomWeaponTrail : XWeaponTrail
    {
        public ColorType _saberType;
        public ColorManager _colorManager;
        public Color _multiplierSaberColor;
        public Color _customColor;
        public Material _customMaterial;

        protected override Color color
        {
            get
            {
                if (_saberType.Equals(ColorType.LeftSaber) && _colorManager != null)
                {
                    return _colorManager.ColorForSaberType(Saber.SaberType.SaberA) * _multiplierSaberColor;
                }
                else if (_saberType.Equals(ColorType.RightSaber) && _colorManager != null)
                {
                    return _colorManager.ColorForSaberType(Saber.SaberType.SaberB) * _multiplierSaberColor;
                }
                else
                {
                    return _customColor * _multiplierSaberColor;
                }
            }
        }

        public void init(XWeaponTrailRenderer TrailRendererPrefab, ColorManager colorManager, Transform PointStart, Transform PointEnd, Material TrailMaterial, Color TrailColor, int Length, Color multiplierSaberColor, ColorType colorType)
        {
            ReflectionUtil.SetPrivateField(this, "_pointStart", PointStart);
            ReflectionUtil.SetPrivateField(this, "_pointEnd", PointEnd);
            ReflectionUtil.SetPrivateField(this, "_maxFrame", Length);
            ReflectionUtil.SetPrivateField(this, "_trailRendererPrefab", TrailRendererPrefab);

             _colorManager = colorManager;
             _multiplierSaberColor = multiplierSaberColor;
             _customColor = TrailColor;
             _customMaterial = TrailMaterial;
             _saberType = colorType;
        }

        public new void Start()
        {
            base.Start();            

            ReflectionUtil.GetPrivateField<MeshRenderer>(ReflectionUtil.GetPrivateField<XWeaponTrailRenderer>(this, "_trailRenderer"), "_meshRenderer").material = _customMaterial;
        }

        public void SetColor(Color newColor)
        {
            _customColor = newColor;
        }

        public void SetMaterial(Material newMaterial)
        {
             _customMaterial = newMaterial;
             ReflectionUtil.GetPrivateField<MeshRenderer>(ReflectionUtil.GetPrivateField<XWeaponTrailRenderer>(this, "_trailRenderer"), "_meshRenderer").material = _customMaterial;
        }
    }
}
