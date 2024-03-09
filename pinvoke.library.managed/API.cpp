#include "Person.h"
#include "API.h"
#pragma warning(disable : 4996)
IPerson* createPerson()
{
    return new Person();
}

void configPerson(IPerson* person, ConfigPerson* config_person)
{
    person->setId(config_person->id);
    person->setAge(config_person->age);
    person->setName(config_person->name);
}
ConfigPerson* getPersonInfo(IPerson* person) 
{
    ConfigPerson* config_person = new ConfigPerson();

    config_person->id = person->getId();
    config_person->age = person->getAge();
    config_person->name = person->getName();

    return config_person;
}

void destroyPerson(IPerson* person) 
{
    delete person;
}