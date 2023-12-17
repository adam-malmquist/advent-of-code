#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

typedef struct {
    char cards[5];
    int bid;
} Hand;

typedef enum { HighCard, OnePair, TwoPair, ThreeOfAKind, FullHouse, FourOfAKind, FiveOfAKind } Strength;

int get_index(char ch);
Strength get_strength(int cards[13]);

int cmpfunc (const void * a, const void * b) {
    Hand *h1 = (Hand*)a, *h2 = (Hand*)b;
    return 0;
}

int main()
{
    FILE *file = fopen("input/07", "r");

    if (file == NULL) {
        printf("File not found.\n");
        return 1;
    }

    char buffer[20];
    
    int n = 0;
    Hand hands[1000];

    while (fgets(buffer, 20, file) != NULL) {
        char *ch = strtok(buffer, " ");
        strcpy(hands[n].cards, ch);
        ch = strtok(NULL, " ");
        hands[n].bid = atoi(ch);
        ++n;
    }

    fclose(file);

    int cards[13] = {0};

    for (int i = 0; i < 5; ++i) {
        ++(cards[get_index(hands[0].cards[i])]);
    }

    Strength strength = get_strength(cards);

    int result = 0;

    printf("Answer: %d\n", result);

    return 0;
}

int get_index(char ch) {
    switch (ch) {
        case '2': return 0;
        case '3': return 1;
        case '4': return 2;
        case '5': return 3;
        case '6': return 4;
        case '7': return 5;
        case '8': return 6;
        case '9': return 7;
        case 'T': return 8;
        case 'J': return 9;
        case 'Q': return 10;
        case 'K': return 11;
        case 'A': return 12;
    }
    return -1;
}

Strength get_strength(int cards[13]) {
    bool three_of_a_kind = false;
    bool pair = false;
    for (int i = 0; i < 13; ++i) {
        if (cards[i] == 5)
            return FiveOfAKind;
        if (cards[i] == 4)
            return FourOfAKind;
        if (cards[i] == 3) {
            if (pair)
                return FullHouse;
            three_of_a_kind = true;
        }
        else if (cards[i] == 2) {
            if (pair)
                return TwoPair;
            if (three_of_a_kind)
                return FullHouse;
            pair = true;
        }
    }
    if (three_of_a_kind)
        return ThreeOfAKind;
    if (pair)
        return OnePair;
    return HighCard;
}
