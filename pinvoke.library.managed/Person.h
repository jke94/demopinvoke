#ifndef PERSON_H
#define PERSON_H

#include <thread>

#include "API.h"
#include "Logger_api.h"

class Person : public IPerson
{
    private:
        int id_;
        int age_;
        char* name_;
        int _ppm;
        int _milliseconds_notification_time = 1000;
        std::thread _thread_life;
        
        void set_ppm(int ppm);
        void do_live();

    public:
        Person();
        ~Person();
        void setId(int id) override;
        void setAge(int age) override;
        void setName(char* name) override;
        int getId() override;
        int getAge() override;
        char* getName() override;
        int get_ppm() override;
};

#endif