#ifndef LOGGER_H
#define LOGGER_H

class ILogger
{
public:
	virtual ~ILogger() {}
	virtual void log_message(const char* str) = 0;
};

class Logger : public ILogger
{
private:
	void(*native_logger_callback_)(const char* str);

public:
	Logger(void(*native_logger_callback)(const char* str));
	~Logger();
	void log_message(const char* str) override;
};


#endif