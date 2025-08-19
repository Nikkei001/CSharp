import os
from readers import process_folder, merge_and_save_data

# --- 配置区域 ---
folder_path = 'd:/Users/Administrator/Desktop/常用路径/每周_PMC发货表原表收集'
output_path = 'd:/Users/Administrator/Desktop/所有文件合并处理后.xlsx'
required_columns = ['辅助列-排序', '销售', '产品编号', '站点', '区域', '销售确认发货']
# --- 配置区域结束 ---

def main():
    """
    主函数，协调整个数据处理流程。
    """
    print(f"开始处理文件夹: {folder_path}")
    
    # 1. 遍历文件夹并处理所有Excel文件
    all_filtered_data = process_folder(folder_path, required_columns)
    
    print("-" * 20)
    
    # 2. 合并数据、生成报告并保存
    merge_and_save_data(all_filtered_data, output_path)

if __name__ == "__main__":
    main()
