#include <chrono>
#include <iostream>
#include <mutex>
#include <sstream>
#include <random>

#include "Person.h"

#define PERSON_PPM_MIN  60
#define PERSON_PPM_MAX  180

std::mutex person_mutex;

void Person::do_live()
{
    do
    {
        std::this_thread::sleep_for(std::chrono::milliseconds(_milliseconds_notification_time));

        int tmp_ppm = PERSON_PPM_MIN + (std::rand() % (PERSON_PPM_MAX - PERSON_PPM_MIN + 1));
        set_ppm(tmp_ppm);

        if (_person_monitor_callback)
        {
            _person_monitor_callback(name_, tmp_ppm);
        }
    } 
    while (_thread_life.joinable());
}

Person::Person()
{
    _thread_life = std::thread(&Person::do_live, this);

    std::stringstream ss;
    ss << "Called constructor, object: " << this;
    std::string msg = ss.str();

    WRITE_INFO(msg);
}

Person::~Person()
{
    std::stringstream ss;


    if (_thread_life.joinable())
    {
        ss << "Deatached thread: " << _thread_life.get_id();
        WRITE_INFO(ss.str());
        ss.str(std::string());
    }

    _thread_life.detach();

    if (!_thread_life.joinable())
    {
        ss << "Deatached thread successfully.";
        WRITE_INFO(ss.str());
        ss.str(std::string());
    }

    ss.str(std::string());
    ss << "Called destructor, object: " << this;
    std::string msg = ss.str();
    WRITE_INFO(ss.str());
}

void Person::setId(int id)
{
    id_ = id;
}

void Person::setAge(int age)
{
    age_ = age;
}

void Person::setName(char* name)
{
    name_ = name;
}

void Person::setPersonMonitorCallback(PERSON_MONITOR_CALLBACK person_monitor_callback)
{
    std::lock_guard<std::mutex> guard(person_mutex);

    _person_monitor_callback = person_monitor_callback;
}

int Person::getId()
{
    std::lock_guard<std::mutex> guard(person_mutex);

    return id_;
}

int Person::getAge()
{
    std::lock_guard<std::mutex> guard(person_mutex);

    return age_;
}

char* Person::getName()
{
    std::lock_guard<std::mutex> guard(person_mutex);

    return name_;
}

void Person::set_ppm(int ppm)
{
    std::lock_guard<std::mutex> guard(person_mutex);

    _ppm = ppm;
}

int Person::get_ppm()
{
    std::lock_guard<std::mutex> guard(person_mutex);

    return _ppm;
}
