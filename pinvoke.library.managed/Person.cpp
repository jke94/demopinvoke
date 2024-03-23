#include <iostream>
#include <sstream>
#include "Person.h"

Person::Person()
{
    std::stringstream ss;
    ss << "Called constructor, object: " << this;
    std::string msg = ss.str();

    WRITE_INFO(msg);
}

Person::~Person()
{
    std::stringstream ss;
    ss << "Called destructor, object: " << this;
    std::string msg = ss.str();

    WRITE_INFO(msg);
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