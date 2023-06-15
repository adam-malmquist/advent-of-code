#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int cmpfunc (const void * a, const void * b) {
   return ( *(int*)a - *(int*)b );
}

int main()
{
    FILE* ptr;
	char buf[10];
	int cals = 0;
	int max[4] = {0,0,0,0};
	int n = 0;

	ptr = fopen("1a.txt", "r");

    if (ptr == NULL)
    {
        printf("error! :)");
		exit(1);
    }

	while (NULL != fgets(buf, 10, ptr))
	{
		n  = atoi(buf);
		if (n >  0)
		{
			cals += n;
		}
		else
		{
			max[0] = cals;
			qsort(max, 4, sizeof(int), cmpfunc);
			cals = 0;
		}
	}

	fclose(ptr);

	printf("max: %d ", max[1] + max[2] + max[3]);

    return 0;
}
