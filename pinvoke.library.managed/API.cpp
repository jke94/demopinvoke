#include <memory>

#include "API.h"
#include "Logger_api.h"
#include "Person.h"

#pragma warning(disable : 4996)

void setUpLogCallback(void(*log_callback)(const char* str))
{
    init_logger(log_callback);

    WRITE_INFO("Initalize native logger.");
}

void disposeLogCallback()
{
    WRITE_INFO("Dispose native logger.");

    end_logger();
}

IPerson* createPerson()
{
    WRITE_INFO("Creating person...");

    return new Person();
}

void configPerson(IPerson* person, ConfigPerson* config_person)
{
    person->setId(config_person->id);
    person->setAge(config_person->age);
    person->setName(config_person->name);
}
void getPersonInfo(IPerson* person, ConfigPerson* config_person)
{
    config_person->id = person->getId();
    config_person->age = person->getAge();
    config_person->name = person->getName();
}

void destroyPerson(IPerson* person) 
{
    delete person;
}