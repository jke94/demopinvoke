#ifndef ILOGGER_H
#define ILOGGER_H

enum class LOG_LEVEL
{
	DEBUG,
	INFO,
	WARNING,
	ERROR
};

class ILogger
{
public:
	virtual ~ILogger() {}
	virtual void log_debug_message(const char* file, const char* function, const int line, const char* msg) = 0;
	virtual void log_info_message(const char* file, const char* function, const int line, const char* msg) = 0;
	virtual void log_warning_message(const char* file, const char* function, const int line, const char* msg) = 0;
	virtual void log_error_message(const char* file, const char* function, const int line, const char* msg) = 0;
};

#endif
