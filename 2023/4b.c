#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define NWIN 10
#define NOURS 25
#define CARDS 213

int main()
{
    FILE *file;
    char buffer[1000];

    int result = 0;
    int winning[NWIN];
    int cards[CARDS];
    int card = 0;

    for (int i = 0; i < CARDS; ++i)
        cards[i] = 1;

    file = fopen("input/04", "r");

    if (file == NULL) {
        printf("File not found.\n");
        return 1;
    }

    while (fgets(buffer, 1000, file) != NULL) {
        char *ch = buffer;
        while (*ch != ':')
            ++ch;
        ++ch;

        ch = strtok(ch, " ");
        for (int i = 0; i < NWIN; ++i) {
            winning[i] = atoi(ch);

            ch = strtok(NULL, " ");
        }

        ch = strtok(NULL, " ");

        int wins = 0;

        for (int i = 0; i < NOURS; ++i) {
            int n = atoi(ch);
            for (int j = 0; j < NWIN; ++j)
                if (winning[j] == n)
                    ++wins;

            ch = strtok(NULL, " ");
        }

        if (wins > 0) {
            for (int i = 1; i <= wins; ++i)
                cards[card+i] += cards[card];
        }

        result += cards[card];

        ++card;
    }

    fclose(file);

    printf("Answer: %d\n", result);

    return 0;
}
