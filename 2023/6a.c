#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define BUFFER_SIZE 1000
#define RACES 4

int main()
{
    FILE *file;
    char buffer[BUFFER_SIZE];

    int time[4];
    int dist[4];

    file = fopen("input/06", "r");

    if (file == NULL) {
        printf("File not found.\n");
        return 1;
    }

    char *ch = buffer;
    ch = fgets(buffer, BUFFER_SIZE, file);
    ch = strtok(ch, " ");
    for (int i = 0; i < RACES; ++i) {
        ch = strtok(NULL, " ");
        time[i] = atoi(ch);
    }

    ch = fgets(buffer, BUFFER_SIZE, file);
    ch = strtok(ch, " ");
    for (int i = 0; i < RACES; ++i) {
        ch = strtok(NULL, " ");
        dist[i] = atoi(ch);
    }

    fclose(file);

    int result = 1;

    for (int i = 0; i < RACES; ++i) {
        int n = 0;
        for (int j = 1; j < time[i]; ++j)
            if (j * (time[i] - j) > dist[i])
                ++n;
        result *= n;
    }

    printf("Answer: %d\n", result);

    return 0;
}
