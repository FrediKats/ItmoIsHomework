#pragma once

#include "device.h"

class device_provider
{
public:
	device select_device(int requested_index);
};
