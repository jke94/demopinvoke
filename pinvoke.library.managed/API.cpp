#include <memory>

#include "API.h"
#include "Logger.h"
#include "Person.h"

#pragma warning(disable : 4996)

ILogger* logger_ = nullptr;

void log_message(const char* str)
{
    logger_->log_message(str);
}

void setUpLogCallback(void(*log_callback)(const char* str))
{
    logger_ = new Logger(log_callback);

    log_message("Initalize native logger.");
}

void disposeLogCallback()
{
    log_message("Dispose native logger.");
    
    delete logger_;
}

IPerson* createPerson()
{
    log_message("Creating person...");

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