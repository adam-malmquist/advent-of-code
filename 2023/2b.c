#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main()
{
    FILE *file;
    char buffer[1000];

    file = fopen("input/02", "r");

    if (file == NULL) {
        printf("File not found.\n");
        return 1;
    }

    int id = 0;
    int result = 0;

    char *t1, *t2, *t3, *t4,
         *p1, *p2, *p3, *p4;

    int r,g,b;

    while (fgets(buffer, 1000, file) != NULL) {
        r=g=b=0;
        ++id;
        t1 = strtok_r(buffer, ":", &p1);
        t1 = strtok_r(NULL, ":", &p1);

        t2 = strtok_r(t1, ";", &p2);
        while (t2 != NULL) {
            t3 = strtok_r(t2, ",", &p3);
            while (t3 != NULL) {
                t4 = strtok_r(t3, " \n", &p4);

                int value = atoi(t4);
                t4 = strtok_r(NULL, " \n", &p4);

                if (strcmp(t4, "red") == 0) {
                    if (value > r)
                        r = value;
                }
                else if (strcmp(t4, "green") == 0) {
                    if (value > g)
                        g = value;
                }
                else if (strcmp(t4, "blue") == 0) {
                    if (value > b)
                        b = value;
                }

                t3 = strtok_r(NULL, ",", &p3);
            }
            t2 = strtok_r(NULL, ";", &p2);
        }

        result += r*g*b;
    }

    fclose(file);

    printf("Answer: %d\n", result);

    return 0;
}
