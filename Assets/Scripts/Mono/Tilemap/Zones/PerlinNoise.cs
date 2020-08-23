using UnityEngine;

public class PerlinNoise
{
    long seed;

    public PerlinNoise(long seed)
    {
        this.seed = seed;
    }

    private int GetRandom(int x, int range)
    {
        return (int)((x + seed)^5) % range;
    }

    public int GetNoise(int x, int range)
    {
        int chunkSize = 16;
        float noise = 0;

        while(chunkSize > 0)
        {
            int chunkIndex = x / chunkSize;

            float progress = (x % chunkSize) / (chunkSize * 1f);

            float l_random = GetRandom(chunkIndex, range);
            float r_random = GetRandom(chunkIndex + 1, range);

            noise += ((1 - progress) * l_random) + (progress * r_random);

            //integer division so it'll reach 0
            chunkSize /= 2;
            range /= 2;

            range = Mathf.Max(1, range);
        }

        return (int)noise;
    }
}
