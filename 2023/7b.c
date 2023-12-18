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
        case 'J': return 0;
        case '2': return 1;
        case '3': return 2;
        case '4': return 3;
        case '5': return 4;
        case '6': return 5;
        case '7': return 6;
        case '8': return 7;
        case '9': return 8;
        case 'T': return 9;
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

    bool five_of_a_kind = false;
    bool four_of_a_kind = false;
    bool three_of_a_kind = false;
    int pairs = 0;

    int jokers = count[0];

    for (int i = 1; i < 13; ++i) {
        if (count[i] == 5)
            five_of_a_kind = true;
        else if (count[i] == 4)
            four_of_a_kind = true;
        else if (count[i] == 3)
            three_of_a_kind = true;
        else if (count[i] == 2)
            ++pairs;
    }

    if (five_of_a_kind)
        return FiveOfAKind;

    if (four_of_a_kind) {
        if (jokers == 1)
            return FiveOfAKind;
        return FourOfAKind;
    }

    if (three_of_a_kind) {
        if (pairs == 1)
            return FullHouse;
        if (jokers == 2)
            return FiveOfAKind;
        if (jokers == 1)
            return FourOfAKind;
        return ThreeOfAKind;
    }

    if (pairs == 2) {
        if (jokers == 1)
            return FullHouse;
        return TwoPair;
    }

    if (pairs == 1) {
        if (jokers == 3)
            return FiveOfAKind;
        if (jokers == 2)
            return FourOfAKind;
        if (jokers == 1)
            return ThreeOfAKind;
        return OnePair;
    }

    switch (jokers) {
        case 5: return FiveOfAKind;
        case 4: return FiveOfAKind;
        case 3: return FourOfAKind;
        case 2: return ThreeOfAKind;
        case 1: return OnePair;
    }

    return HighCard;
}
