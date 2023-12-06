#include <ctype.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int calc(char *str, int col, int *parts);
int calc2(char *str, int col, int *parts);

int main()
{
    FILE *file;
    char buffer[1000];
    char map[141][141] = {0};

    int result = 0;

    file = fopen("input/03", "r");

    if (file == NULL) {
        printf("File not found.\n");
        return 1;
    }

    int row = 0, col = 0;

    while (fgets(buffer, 1000, file) != NULL) {
        memcpy(map[row], buffer, 140);
        map[row][140] = '\0';
        ++row;
    }

    for (row = 0; row < 140; ++row) {
        for (col = 0; col < 140; ++col) {
            char ch = map[row][col];
            if (ch == '*') {
                int parts = 0;
                int ratio = 1;
                if (row > 0)
                    ratio *= calc(map[row-1], col, &parts);
                ratio *= calc(map[row], col, &parts);
                if (row < 140)
                    ratio *= calc(map[row+1], col, &parts);

                if (parts == 2)
                    result += ratio;
            }
        }
    }


    fclose(file);

    printf("Answer: %d\n", result);

    return 0;
}

int calc(char *str, int col, int *parts) {
    int res = 1;

    if (isdigit(str[col])) {
        res *= calc2(str, col, parts);
    }
    else {
        if (col > 0) {
            res *= calc2(str, col-1, parts);
        }
        if (col < 139)
            res *= calc2(str, col+1, parts);
    }

    return res;
}

int calc2(char *str, int col, int *parts) {
    char *ch = &str[col];

    if (!isdigit(*ch))
        return 1;

    while (ch != str) {
        --ch;
        if (!isdigit(*ch)) {
            ++ch;
            break;
        }
    }

    (*parts)++;

    return atoi(ch);
}
