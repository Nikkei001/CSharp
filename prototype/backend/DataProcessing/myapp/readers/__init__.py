from .excel_reader import read_excel_file_custom
from .process_each_file import process_folder
from .data_merger import merge_and_save_data

__all__ = [
    'read_excel_file_custom',
    'process_folder',
    'merge_and_save_data'
]
# This file makes the 'readers' directory a Python package.