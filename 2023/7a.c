#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

typedef enum { HighCard, OnePair, TwoPair, ThreeOfAKind, FullHouse, FourOfAKind, FiveOfAKind } Strength;

typedef struct {
    char cards[5];
    int bid;
    int cards_value[5];
    Strength strength;
} Hand;

int get_index(char ch);
Strength get_strength(int cards[5]);

int cmpfunc (const void * a, const void * b) {
    Hand *h1 = (Hand*)a, *h2 = (Hand*)b;

    if (h1->strength > h2->strength)
        return 1;
    if (h1->strength < h2->strength)
        return -1;

    for (int i = 0; i < 5; ++i) {
        if (h1->cards_value[i] > h2->cards_value[i])
            return 1;
        if (h1->cards_value[i] < h2->cards_value[i])
            return -1;
    }

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
    
    Hand hands[1000];
    int n = 0;

    while (fgets(buffer, 20, file) != NULL) {
        char *ch = strtok(buffer, " ");
        strcpy(hands[n].cards, ch);
        ch = strtok(NULL, " ");
        hands[n].bid = atoi(ch);

        for (int i = 0; i < 5; ++i)
            hands[n].cards_value[i] = get_index(hands[n].cards[i]);
        hands[n].strength = get_strength(hands[n].cards_value);

        ++n;
    }

    fclose(file);

    qsort(&hands, n, sizeof(Hand), cmpfunc);

    long long result = 0;

    for (int i = 0; i < n; ++i)
        result += (i+1) * hands[i].bid;

    printf("Answer: %lld\n", result);

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

Strength get_strength(int cards[5]) {
    int count[13] = {0};

    for (int i = 0; i < 5; ++i)
        ++(count[cards[i]]);

    bool three_of_a_kind = false;
    bool pair = false;

    for (int i = 0; i < 13; ++i) {
        if (count[i] == 5)
            return FiveOfAKind;
        if (count[i] == 4)
            return FourOfAKind;
        if (count[i] == 3) {
            if (pair)
                return FullHouse;
            three_of_a_kind = true;
        }
        else if (count[i] == 2) {
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
