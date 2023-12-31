﻿using SmartAddresser.Editor.Core.Models.Shared.AssetGroups.AssetFilterImpl;
using SmartAddresser.Editor.Foundation.CustomDrawers;
using SmartAddresser.Editor.Foundation.ListableProperty;
using UnityEditor;
using UnityEngine;

namespace SmartAddresser.Editor.Core.Tools.Addresser.Shared.AssetGroups.AssetFilterDrawer
{
    [CustomGUIDrawer(typeof(ObjectBasedAssetFilter))]
    internal sealed class ObjectBasedAssetFilterDrawer : GUIDrawer<ObjectBasedAssetFilter>
    {
        private ObjectListablePropertyGUI _listablePropertyGUI;

        public override void Setup(object target)
        {
            base.Setup(target);
            _listablePropertyGUI = new ObjectListablePropertyGUI(ObjectNames.NicifyVariableName(nameof(Target.Object)),
                Target.Object, typeof(Object), false);
        }

        protected override void GUILayout(ObjectBasedAssetFilter target)
        {
            target.FolderTargetingMode =
                (FolderTargetingMode)EditorGUILayout.EnumPopup("Folder Targeting Mode", target.FolderTargetingMode);
            _listablePropertyGUI.DoLayout();
        }
    }
}
