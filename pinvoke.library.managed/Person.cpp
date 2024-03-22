#include "Person.h"
#include <iostream>

Person::Person()
{
    std::cout << "[NATIVE] Called constructor over " << this << "." << std::endl;
}

Person::~Person()
{
    std::cout << "[NATIVE] Called destructor over " << this << "." << std::endl;
}

void Person::setId(int id)
{
    id_ = id;
}

void Person::setAge(int age)
{
    age_ = age;
}

void Person::setName(char* name)
{
    name_ = name;
}

int Person::getId()
{
    return id_;
}

int Person::getAge()
{
    return age_;
}

char* Person::getName()
{
    return name_;
}