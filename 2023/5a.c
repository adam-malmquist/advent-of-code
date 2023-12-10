#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define SEEDS 20
#define MAPS 7

struct MapEntry {
    long long dst;
    long long src;
    long long range;
};

int main()
{
    FILE *file;
    char buffer[1000];

    long long result = INT_MAX;

    file = fopen("input/05", "r");

    if (file == NULL) {
        printf("File not found.\n");
        return 1;
    }

    fgets(buffer, 1000, file);

    long long seeds[SEEDS];

    char *pch = buffer;
    pch = strtok(pch, " ");
    for (int i = 0; i < SEEDS; ++i) {
        pch = strtok(NULL, " ");
        seeds[i] = atoll(pch);
    }
    fgets(buffer, 1000, file);

    struct MapEntry maps[MAPS][60] = {0};
    int mapLengths[MAPS] = {0};

    for (int i = 0; i < MAPS; ++i) {
        fgets(buffer, 1000, file);

        int x = 0;

        while (fgets(buffer, 1000, file) != NULL) {
            if (strcmp(buffer, "\n") == 0) {
                break;
            }

            pch = strtok(buffer, " ");
            maps[i][x].dst = atoll(pch);
            pch = strtok(NULL, " ");
            maps[i][x].src = atoll(pch);
            pch = strtok(NULL, " ");
            maps[i][x].range = atoll(pch);

            ++x;

        }

        mapLengths[i] = x;
    }

    for (int seed = 0; seed < SEEDS; ++seed) {
        long long n = seeds[seed];

        for (int i = 0; i < MAPS; ++i) {
            for (int j = 0; j < mapLengths[i]; ++j) {
                struct MapEntry *entry = &maps[i][j];
                long long offset = n - entry->src;
                if (offset >= 0 && offset < entry->range) {
                    n = entry->dst + offset;
                    break;
                }
            }
        }

        if (n < result)
            result = n;
    }


    fclose(file);

    printf("Answer: %lld\n", result);

    return 0;
}
