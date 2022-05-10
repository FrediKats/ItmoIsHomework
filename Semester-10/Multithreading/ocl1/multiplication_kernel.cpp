#include "multiplication_kernel.h"

multiplication_kernel::multiplication_kernel(const ocl1::execution_context& execution_context_instance,
	cl_kernel kernel): kernel_contract<multiplication_kernel_argument, multiplication_kernel_response>(
	execution_context_instance, kernel)
{
}
