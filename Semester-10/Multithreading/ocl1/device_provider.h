#pragma once

#include "device.h"

namespace ocl1
{
	class device_provider
	{
	public:
		explicit device_provider(bool trace_detailed_info);

		device select_device(int requested_index) const;

	private:
		const bool trace_detailed_info_;
	};
}
