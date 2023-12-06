#include <ctype.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int calc(char *str, int col);

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
            if (!isdigit(ch) && ch != '.') {
                if (row > 0)
                    result += calc(map[row-1], col);
                result += calc(map[row], col);
                if (row < 140)
                    result += calc(map[row+1], col);
            }
        }
    }


    fclose(file);

    printf("Answer: %d\n", result);

    return 0;
}

int calc(char *str, int col) {
    int res = 0;

    for (int i = -1; i <= 1; ++i) {

        if (col+i < 0 || col+i >= 140)
            continue;

        char *ch = &str[col+i];
        if (isdigit(*ch)) {
            while (ch != str) {
                --ch;
                if (!isdigit(*ch)) {
                    ++ch;
                    break;
                }
            }
            res += atoi(ch);
            while (isdigit(*ch)) {
                *ch = '.'; 
                ++ch;
            }
        }
    }

    return res;
}
