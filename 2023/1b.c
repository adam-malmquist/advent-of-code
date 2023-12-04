#include <ctype.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

char get_digit(char *str);

int main()
{
    FILE *file;
    char buffer[1000];

    file = fopen("input/01", "r");

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
            else {
                char n = get_digit(&buffer[i]);
                if (n != '\0') {
                    if (digits[0] == '\0')
                        digits[0] = n;
                    digits[1] = n;
                }
            }
        }

        sum += atoi(digits);
    }

    fclose(file);

    printf("Answer: %d\n", sum);

    return 0;
}

char get_digit(char *str) {
   if (strncmp(str, "one", 3) == 0)
        return '1';
   if (strncmp(str, "two", 3) == 0)
        return '2';
   if (strncmp(str, "three", 5) == 0)
        return '3';
   if (strncmp(str, "four", 4) == 0)
        return '4';
   if (strncmp(str, "five", 4) == 0)
        return '5';
   if (strncmp(str, "six", 3) == 0)
        return '6';
   if (strncmp(str, "seven", 5) == 0)
        return '7';
   if (strncmp(str, "eight", 5) == 0)
        return '8';
   if (strncmp(str, "nine", 4) == 0)
        return '9';

    return '\0';
}
