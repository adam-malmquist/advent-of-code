#include <ctype.h>
#include <stdio.h>
#include <stdlib.h>

int main()
{
    FILE *file;
    char buffer[1000];

    file = fopen("input/1.txt", "r");

    if (file == NULL) {
        printf("File not found.\n");
        return 1;
    }

    int sum = 0;
    char *c;
    char digits[3];

    while (fgets(buffer, 1000, file) != NULL) {
        digits[0] = '\0';
        digits[1] = '\0';

        for (int i = 0; i < 1000; ++i) {
            if (buffer[i] == '\0')
                break;
            if (isdigit(buffer[i])) {
                if (digits[0] == '\0')
                    digits[0] = buffer[i];
                digits[1] = buffer[i];
            }
        }

        sum += atoi(digits);
    }

    fclose(file);

    printf("Answer: %d\n", sum);

    return 0;
}
