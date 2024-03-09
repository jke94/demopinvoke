#ifndef API_H
#define API_H

#include <string>
#include <memory>

#ifdef _WIN32
#   define EXPORT __declspec(dllexport)
#else
#   define EXPORT __declspec(dllimport)
#endif

struct ConfigPerson
{
    int id;
    int age;
    char* name;
};

class IPerson 
{
    public:
        virtual ~IPerson() {}
        virtual void setId(int id) = 0;
        virtual void setAge(int age) = 0;
        virtual void setName(char* name) = 0;
        virtual int getId() = 0;
        virtual int getAge() = 0;
        virtual char* getName() = 0;
};

EXPORT IPerson* createPerson();
EXPORT void configPerson(IPerson* person, ConfigPerson* config_person);
EXPORT ConfigPerson* getPersonInfo(IPerson* person);
EXPORT void destroyPerson(IPerson* person);

#endif