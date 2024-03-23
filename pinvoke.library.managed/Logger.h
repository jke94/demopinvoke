#ifndef LOGGER_H
#define LOGGER_H

#include <cstring>
#include <string>
#include "ILogger.h"

class Logger : public ILogger
{
private:
	void(*native_logger_callback_)(const char* str);
	std::string log(const char* file, const char* function, const int line, const char* msg);

public:
	Logger(void(*native_logger_callback)(const char* str));
	~Logger();
	void log_message(const char* file, const char* function, const int line, const char* msg) override;
};

#endif