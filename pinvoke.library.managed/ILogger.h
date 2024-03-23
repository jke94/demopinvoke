#ifndef ILOGGER_H
#define ILOGGER_H

class ILogger
{
public:
	virtual ~ILogger() {}
	virtual void log_message(const char* file, const char* function, const int line, const char* msg) = 0;
};

#endif
