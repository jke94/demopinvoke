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

void setPersonInfo(IPerson* person, PersonInfo* person_info)
{
    person->setId(person_info->id);
    person->setAge(person_info->age);
    person->setName(person_info->name);
}
void getPersonInfo(IPerson* person, PersonInfo* person_info)
{
    person_info->id = person->getId();
    person_info->age = person->getAge();
    person_info->name = person->getName();
}

void setPersonMonitor(IPerson* person, PERSON_MONITOR_CALLBACK person_monitor_callback)
{
    person->setPersonMonitorCallback(person_monitor_callback);
}

void destroyPerson(IPerson* person) 
{
    delete person;
}