using System;
using System.Collections;

public class MapUtility
{
    public static T[] ShuffleArr<T>(T[] array, int seed)
    {
        System.Random prng = new System.Random(seed);
        for (int i = 0; i <  array.Length -1; ++i)
        {
            int randindex = prng.Next(i, array.Length);
            T tempItem = array[randindex];
            array[randindex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }

    public static float linearSin(float n) // 주기 0.0~1.0 위상 -1 ~ 1 시작 0
    {
        float x = (n - (int)n) - 0.25f;
        return x > 0 ? -4 * x + 1: 4 * x + 1;
    }

    public static float sin(float n) // 주기 0.0~1.0 위상 -1 ~ 1 시작 0
    { 
        return (float)(Math.Sin((double)n * Math.PI * 2));
    }

    public static float pi
    {
        get{
            return (float)Math.PI;
        }
    }
    public static string getRoomName(int i, int j){
        return "tile_" + (i+1).ToString() + "_" + (j+1).ToString();
    }
}