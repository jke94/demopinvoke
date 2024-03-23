#ifndef LOGGER_H
#define LOGGER_H

#include <cstring>
#include <string>
#include "ILogger.h"

class Logger : public ILogger
{
private:
	void(*native_logger_callback_)(const char* str);

	std::string log_level_to_str(LOG_LEVEL log_level);
	std::string log(const char* file, const char* function, const int line, const char* msg, LOG_LEVEL log_level);

public:

	Logger(void(*native_logger_callback)(const char* str));
	~Logger();
	void log_debug_message(const char* file, const char* function, const int line, const char* msg) override;
	void log_info_message(const char* file, const char* function, const int line, const char* msg) override;
	void log_warning_message(const char* file, const char* function, const int line, const char* msg) override;
	void log_error_message(const char* file, const char* function, const int line, const char* msg) override;
};

#endif