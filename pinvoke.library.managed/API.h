#ifndef API_H
#define API_H

#include <string>
#include <memory>

#if defined(_WIN32)
	#define EXPORT __declspec(dllexport)
#elif defined(__GNUC__)
	#define EXPORT __attribute__((visibility("default")))
#else
    #define EXPORT
    #define IMPORT
    #pragma warning Unknown dynamic link import/export semantics.
#endif

struct PersonInfo
{
    int id;
    int age;
    char* name;
};

typedef void (*PERSON_MONITOR_CALLBACK)(char*, int);

class IPerson 
{
    public:
        virtual ~IPerson() {}
        virtual void setId(int id) = 0;
        virtual void setAge(int age) = 0;
        virtual void setName(char* name) = 0;
        virtual void setPersonMonitorCallback(PERSON_MONITOR_CALLBACK person_monitor_callback) = 0;
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
EXPORT void setPersonMonitor(IPerson* person, PERSON_MONITOR_CALLBACK person_monitor_callback);
EXPORT void destroyPerson(IPerson* person);

#endif