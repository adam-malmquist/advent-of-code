#include <ctype.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define BUFFER_SIZE 1000

long long getlld(FILE *file);

int main()
{
    FILE *file = fopen("input/06", "r");

    if (file == NULL) {
        printf("File not found.\n");
        return 1;
    }

    long long
        time = getlld(file),
        dist = getlld(file);

    fclose(file);

    int result = 0;
    for (int i = 1; i < time; ++i)
        if (i * (time - i) > dist)
            ++result;

    printf("Answer: %d\n", result);

    return 0;
}

long long getlld(FILE *file) {
    char buffer[BUFFER_SIZE];
    char buffer2[20] = {0};
    int l = 0;
    char *ch = fgets(buffer, BUFFER_SIZE, file);

    while (*ch != '\n') {
       if (isdigit(*ch))
            buffer2[l++] = *ch;
        ++ch;
    }

    long long res = atoll(buffer2);
    return res;
}
