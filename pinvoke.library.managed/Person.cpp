#include <iostream>
#include "Person.h"
#include <iostream>

Person::Person()
{
}

Person::~Person()
{

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