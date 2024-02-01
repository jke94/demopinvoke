#pragma once

#ifdef DLLPROJECT_EXPORTS
#   define EXPORT __declspec(dllexport)
#else
#   define EXPORT __declspec(dllimport)
#endif

class Dummy
{
public:
    int dummy_function(int a);
};

struct Person
{
    int id;
    int age;
};

extern "C" EXPORT Dummy* createHoge();
extern "C" EXPORT void freeHoge(Dummy * instance);
extern "C" EXPORT int getResult(Dummy * instance, int a);
extern "C" EXPORT void createPerson(Dummy * instance, Person * person);