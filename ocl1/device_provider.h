#pragma once

#include "device.h"

namespace ocl1
{
	class device_provider
	{
	public:
		device select_device(int requested_index);
	};
}
