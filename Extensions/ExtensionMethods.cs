﻿//       __    __     ______     ______   ______     __  __     ______
//      /\ "-./  \   /\  __ \   /\__  _\ /\  ___\   /\ \_\ \   /\  __ \
//      \ \ \-./\ \  \ \  __ \  \/_/\ \/ \ \ \____  \ \  __ \  \ \  __ \
//       \ \_\ \ \_\  \ \_\ \_\    \ \_\  \ \_____\  \ \_\ \_\  \ \_\ \_\
//        \/_/  \/_/   \/_/\/_/     \/_/   \/_____/   \/_/\/_/   \/_/\/_/
//         I  N  D  U  S  T  R  I  E  S             www.matcha.industries

using UnityEngine;
using System.Collections;
using System;
using Rotorz.Tile;

namespace Matcha.Extensions {

    public static class ExtensionMethods
    {
        // transform extensions
        public static void SetX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        public static void SetY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        public static void SetXY(this Transform transform, float x, float y)
        {
            transform.position = new Vector3(x, y, transform.position.z);
        }

        public static void Set(this Transform transform, float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        public static void SetLocalScaleX(this Transform transform, float x)
        {
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }

        public static GameObject GetTile(this Transform transform, TileSystem tileSystem, int xDirection, int yDirection)
        {
            int convertedX = (int) Math.Floor(transform.position.x);
            int convertedY = (int) Math.Ceiling(Math.Abs(transform.position.y));

            TileData tile = tileSystem.GetTile(convertedY + yDirection, convertedX + xDirection);

            if (tile != null)
            {
                return tile.gameObject;
            }

            return null;
        }

        public static GameObject GetTileBelow(this Transform transform, TileSystem tileSystem, int direction)
        {
            int convertedX = (int) Math.Floor(transform.position.x);
            int convertedY = (int) Math.Floor(Math.Abs(transform.position.y));

            TileData tile = tileSystem.GetTile(convertedY, convertedX + direction);

            if (tile != null)
            {
                return tile.gameObject;
            }

            return null;
        }

        public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
        {
            var dir = (body.transform.position - explosionPosition);
            float wearoff = 1 - (dir.magnitude / explosionRadius);
            body.AddForce(dir.normalized * explosionForce * wearoff);
        }

        public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
        {
            var dir = (body.transform.position - explosionPosition);
            float wearoff = 1 - (dir.magnitude / explosionRadius);
            Vector3 baseForce = dir.normalized * explosionForce * wearoff;
            body.AddForce(baseForce);

            float upliftWearoff = 1 - upliftModifier / explosionRadius;
            Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
            body.AddForce(upliftForce);
        }
    }
}