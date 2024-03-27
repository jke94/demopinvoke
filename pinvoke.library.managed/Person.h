#ifndef PERSON_H
#define PERSON_H

#include <thread>

#include "API.h"
#include "Logger_api.h"

class Person : public IPerson
{
    private:
        int id_ = -1;
        int age_ = -1;
        char* name_ = nullptr;
        int _ppm = -1;

        PERSON_MONITOR_CALLBACK _person_monitor_callback = nullptr;
        int _milliseconds_notification_time = 500;
        std::thread _thread_life;
        
        void set_ppm(int ppm);
        void do_live();

    public:
        Person();
        ~Person();
        void setId(int id) override;
        void setAge(int age) override;
        void setName(char* name) override;
        void setPersonMonitorCallback(PERSON_MONITOR_CALLBACK person_monitor_callback) override;
        int getId() override;
        int getAge() override;
        char* getName() override;
        int get_ppm() override;
};

#endif