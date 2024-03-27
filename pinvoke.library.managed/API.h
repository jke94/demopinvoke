#ifndef API_H
#define API_H

#include <string>
#include <memory>

#ifdef _WIN32
#   define EXPORT __declspec(dllexport)
#else
#   define EXPORT __declspec(dllimport)
#endif

struct PersonInfo
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
        virtual int get_ppm() = 0;
};

EXPORT void setUpLogCallback(void(*log_callback)(const char* str));
EXPORT void disposeLogCallback();

EXPORT IPerson* createPerson();
EXPORT void setPersonInfo(IPerson* person, PersonInfo* person_info);
EXPORT void getPersonInfo(IPerson* person, PersonInfo* person_info);
EXPORT void destroyPerson(IPerson* person);

#endif