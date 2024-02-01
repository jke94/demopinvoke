#pragma once

#include "API.h"
#include <iostream>

Dummy* createHoge()
{
    return new Dummy();
}

void freeHoge(Dummy* instance)
{
    std::cout << "Deleteing Hoge: " << instance << std::endl;

    delete instance;
}

int getResult(Dummy* instance, int a)
{
    return instance->dummy_function(a);
}

void createPerson(Dummy* instance, Person* person)
{
    static_cast<Person*>(person)->id = 1994;
    static_cast<Person*>(person)->age = 15;
}