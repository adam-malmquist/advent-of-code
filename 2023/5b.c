#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define SEEDS 10
#define MAX_SEEDS 1000
#define MAPS 7

typedef struct {
    long long dst;
    long long src;
    long long range;
} MapEntry;

typedef struct {
    long long start;
    long long length;
} Seed;

// int mapCompare(const void *p1, const void *p2) {
//     MapEntry e1 = *(MapEntry *)p1;
//     MapEntry e2 = *(MapEntry *)p2;
//     if (e1.dst == e2.dst)
//         return 0;
//     if (e1.dst > e2.dst)
//         return 1;
//     return -1;
// }

int main()
{
    FILE *file;
    char buffer[1000];

    long long result = LONG_MAX;

    file = fopen("input/05", "r");

    if (file == NULL) {
        printf("File not found.\n");
        return 1;
    }

    fgets(buffer, 1000, file);

    int seedsLength = SEEDS;
    Seed seeds[MAX_SEEDS];

    char *pch = buffer;
    pch = strtok(pch, " ");
    for (int i = 0; i < SEEDS; ++i) {
        pch = strtok(NULL, " ");
        seeds[i].start = atoll(pch);
        pch = strtok(NULL, " ");
        seeds[i].length = atoll(pch);
    }
    fgets(buffer, 1000, file);

    MapEntry maps[MAPS][60] = {0};
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

        //qsort(maps[i], x, sizeof(MapEntry), mapCompare);
        mapLengths[i] = x;
    }

    fclose(file);

    for (int i = 0; i < MAPS; ++i) {
        for (int seed = 0; seed < seedsLength; ++seed) {
            for (int j = 0; j < mapLengths[i]; ++j) {
                MapEntry *e = &maps[i][j];
                Seed *s = &seeds[seed];

                long long
                    a1 = s->start,
                    a2 = s->start + s->length - 1;

                const long long
                    b1 = e->src,
                    b2 = e->src + e->range - 1;

                if (a2 < b1 || a1 > b2)
                    continue;

                if (a1 < b1) {
                    Seed *new = &seeds[seedsLength++];
                    new->start = a1;
                    new->length = b1 - a1;
                    a1 = b1;
                }

                if (a2 > b2) {
                    Seed *new = &seeds[seedsLength++];
                    new->start = b2 + 1;
                    new->length = a2 - b2;
                    a2 = b2;
                }

                s->start = a1;
                s->length = a2 - (a1 - 1);

                s->start = e->dst + (a1 - b1);

                if (seedsLength > MAX_SEEDS) {
                    printf("OVERFLOW");
                    return 0;
                }
            }
        }
    }

    for (int i = 0; i < seedsLength; ++i)
        if (seeds[i].start < result) {
            result = seeds[i].start;
            printf("New result: %lld\n", result);
        }

    printf("Seeds: %d\n", seedsLength);
    printf("Answer: %lld\n", result);

    return 0;
}
