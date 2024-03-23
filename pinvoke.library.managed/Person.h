#ifndef PERSON_H
#define PERSON_H

#include "API.h"
#include "Logger_api.h"

class Person : public IPerson
{
    private:
        int id_;
        int age_;
        char* name_;

    public:
        Person();
        ~Person();
        void setId(int id) override;
        void setAge(int age) override;
        void setName(char* name) override;
        int getId() override;
        int getAge() override;
        char* getName() override;
};

#endif